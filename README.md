# FastFuzzyStringMatcher
FastFuzzyStringMatcher is a BK tree implementation for quick in-memory string matching.
(Also available in [Java](https://github.com/pekoto/FastFuzzyStringMatcher)).

## Features
- Fast, fuzzy, string matching.
- Search based on percentage and edit distance.
- Associate data with string keywords and return both. For example, search for a file name, and return associated file paths.

## Motivation
Although hash maps can be used for exact string matching, and tries can be used for prefix matching, there are few solutions out there for fast matching based on edit distance or percentage difference. Of course, you can search through every string in a collection, comparing its edit distance to the keyword you're searching for, but this tends to be pretty inefficient.

FastFuzzyStringMatcher builds a [BK tree](https://en.wikipedia.org/wiki/BK-tree) to make searching a lot more efficient.

## Setup
The project was built using Visual Studio 2017 and should build cleanly, assuming you have the latest .NET package installed.

There are three projects in the solution:
* Example app (an example application showing how the `StringMatcher` can be used to search a translation memory dictionary)
* FastFuzzyStringMatcher (the main code. `StringMatcher.cs` is the main class you want to look at if you're interested in the code)
* FastFuzzyStringMatcherTests (unit tests)

## NuGet

You can install the package using NuGet too: `Install-Package FastFuzzyStringMatcher`
(Warning: This currently also pulls in some .NET Standard Library .dll references since the project was built as a .NET Standard class library.)

### Usage
Usage is fairly simple:

1. Declare a new instance: `StringMatcher<T> myStringMatcher = new StringMatcher<T>();`
2. Add your data by calling `myStringMatcher.Add(...)`
3. Search for your data by calling `myStringMatcher.Search(...)`

`EditDistanceCalculator.cs` is also public,  so it can also be used independently to calculate the edit distance between two `String` objects.

### Running the tests
`FastFuzzyStringMatcherTests` contains unit tests which demonstrate and verify the functionality of the `StringMatcher` and `EditDistanceCalculator` classes.

These can be run in Visual Studio by right-clicking and selecting Run Tests, or from the Test Explorer.

### Running the example
The `ExampleApp` project shows how the `StringMatcher` can be used to implement a translation memory dictionary with fuzzy matching. `EnglishJapaneseDictionarySearcher.cs` contains the implementation of the translation memory dictionary. `MainForm.cs` is a just a simple form that accepts a search term and minimum match percentage.

Note: This is a rough-and-ready example and does not include threading or error handling. In a real application, you want to do large file reads on a separate thread to keep the UI responsive.

The dictionary loads around 50,000 entries from the [JMDict Project](http://www.edrdg.org/jmdict/j_jmdict.html). `StringMatcher` should be able to handle a lot more than 50,000 translation pairs, but I wanted to keep the download size fairly small.

## How Does It All Work?

### 1. Edit Distance
The fuzzy string matching relies on edit distance.

Edit distance, better known as [Levenshtein distance](https://en.wikipedia.org/wiki/Levenshtein_distance), is the minimum number of edits it takes to turn one string into another, using substitution, insertion, and deletion.

For example, to turn __cat__ into __hate__:
1. cat > hat (substitute c for h)
2. hat > hate (insert e)

Edit distance = 2

The algorithm to calculate this uses dynamic programming to build a matrix of edit distances. [Wiki](https://en.wikipedia.org/wiki/Levenshtein_distance) has a nice explanation and good examples.

`EditDistanceCalculator.java` uses the [iterative with two matrix rows approach](https://en.wikipedia.org/wiki/Levenshtein_distance#Iterative_with_two_matrix_rows). This seems to give the best performance based on some quick tests I ran.

## 2. BK Tree
`StringMatcher` is essentially a [BK tree](https://en.wikipedia.org/wiki/BK-tree) implementation.

In a BK tree, every node is added based on its edit distance from the root.

For example, say we had this collection of words: __hat__, __cat__, __kate__, __ball__, and __bat__.

We start by adding __hat__. It becomes the root:

<img src="https://github.com/pekoto/FastFuzzyStringMatcher/blob/master/images/bk-tree-1.png" width="76" height="100" />

Next we add __cat__. This has an edit distance of 1 from __hat__ (substitute h for c), so we add it as a child with a key of 1:

<img src="https://github.com/pekoto/FastFuzzyStringMatcher/blob/master/images/bk-tree-2.png" width="75" height="196" />

We do the same with __kate__ and __ball__ -- calculate their edit distances respective to the root, and then add them as children with those keys:

<img src="https://github.com/pekoto/FastFuzzyStringMatcher/blob/master/images/bk-tree-4.png" width="322" height="196" />

Finally we add __bat__. But notice that the edit distance is 1, and we already have a child with edit distance 1 -- __cat__. 
No problem. We just move down to cat, calculate the edit distance between __cat__ and __bat__, and add the node as a child of __cat__.

<img src="https://github.com/pekoto/FastFuzzyStringMatcher/blob/master/images/bk-tree-5.png" width="322" height="315" />

Okay, now we're ready to search!

Imagine we accidentally typed in the word __zat__, and we want to get a list of potential corrections for our typo.
Let's say we want to search all of our nodes with a maximum edit distance of 1.

First, we compare __zat__ with our root, __hat__. Sure enough, the edit distance is 1, which is within our threshold, so we add __hat__ to our list of results to return.

<img src="https://github.com/pekoto/FastFuzzyStringMatcher/blob/master/images/search-1.png" width="322" height="355" />

Next, we examine all of the child nodes within the current edit distance +/- our edit distance threshold of 1.

* 1 (current edit distance) - 1 (our threshold) = 0 (min edit distance)
* 1 (current edit distance) + 1 (our threshold) = 2 (max edit distance)

So we'll examine all of the children that have an edit distance between 0-2. That means we'll examine __kate__ and __cat__, but ignore __ball__.
First, let's check out __kate__.

<img src="https://github.com/pekoto/FastFuzzyStringMatcher/blob/master/images/search-2.png" width="354" height="316" />

Oh uh, the edit distance between __zat__ and __kate__ is 2, so we ignore this node, and there are no children, so let's back up.

__cat__ has an edit distance of 1, so let's check it out. The edit distance between __zat__ and __cat__ is also 1, which is within our threshold, so hooray! We have another result.

<img src="https://github.com/pekoto/FastFuzzyStringMatcher/blob/master/images/search-3.png" width="354" height="355" />

Oh yeah, and cat has a child node. We repeat the step we did at the root but using our current node: work out the maximum and minimum threshold based on the edit distance between __zat__ and __cat__, and then examine children within that threshold.

This brings us down to __bat__. We check the edit distance, and again find it's within our threshold.

With that we're done, and we return __hat__, __cat__, and __bat__. We can imagine any of these might be a typo for __zat__. If you wanted to predict which of the three words was most likely meant by the user, you could also take into account which keys are most commonly mistyped. For example, __c__ is closest to __z__ on a standard QWERTY keyboard, so you could guess that they probably meant __cat__.

<img src="https://github.com/pekoto/FastFuzzyStringMatcher/blob/master/images/search-4.png" width="354" height="355" />

Overall, we still ended up searching 80% of our tree, but 80% can still lead to a significant saving if you have, for example, 500,000 strings in your collection.

__Exercise__

What would happen if __zap__ had been added to our BK tree?

## Thoughts
The BK tree is a simple data structure that can deliver decent performance gains when you need to search a large number of strings. They're quick to implement and having fuzzy searching and custom spell checking can be a super nice feature for your application, especially when you're dealing with translation data or you have lots of custom strings that won't be picked up by a standard spell checker, like fund codes, stock ticker symbols, or fictional character names.

Happy searching :)
