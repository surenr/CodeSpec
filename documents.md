---
layout: page
title: Home
---

#Table of content

1. [Read the content](#read-the-content)
    1. [Read the content of](#read-the-content-of)
2. [Content comparison](#content-comparison)
    1. [Content contains](#content-contains)
    2. [Content contains pattern](#content-contains-pattern)
3. Click
    1. Click on
    2. Click and wait
4. Drag and drop
5. Enter value
6. Navigation
    1. Navigate to SUT
    2. Navigate to a sub link under the SUT
    3. Navigate to URL
7. Tabs and Window Manipulations
    1. Switch to tab
    2. Close tab
8. Table Manipulations
    1. Number of rows/columns
    2. Row contains value
9. Visibility  of element
10. Alerts
11. Read URL
    1. Read whole page of URL
    2. Read content of an element in page of URL
12. Variable manipulation
    1. Enter value of variable
    2. Value of variable comparison
13. Frame manipulation
    1. Switch to an iframe
    2. Switch to default content

---

#1. Read the content

###Grammer
Read the content of `<element key>`

### Parameters  
`<element key>` - identifier defined in the repository which  represents a web element

### Example

<pre><code class="language-gherkin">
Read the content of "errorMessage"

or

Get the content of "errorMessage" with the "id" of "errorMessage"
Get the content of "errorMessage" with the "xpath" of "id('errorMessage')/p"

 </code></pre>
 ---


# 2. Content comparison

##2.1 Content contains

###Grammer
The content of `<element key>` is equal to `<expected value>`

#### Parameters  
`<element key>` - identifier defined in the repository which  represents a web element

`<expected value>` - the value expected to be there

#### Example

<pre><code class="language-gherkin">
Content of "errorMessage" contains text "Invalid username"
The content of "errorMessage" contains text "Invalid username"

The content of "errorMessage" with the "id" of "errorMessage" has text "Invalid username"
Content of "errorMessage" with the "xpath" of "id('errorMessage')/p" has text "Invalid username"
 </code></pre>

##2.2 Content contains pattern
The content of page/element contains text with pattern `<text pattern>`

###Grammer
Page contains the text pattern `<text pattern>`

The content of the page contains text pattern  `<text pattern>`

`<element key>` contains the text pattern "You have no messages to show"

The content of the `<element key>` contains text pattern `<text pattern>`

#### Parameters  

`<text pattern>` - the value expected to be there

`<element key>` - identifier defined in the repository which  represents a web element

#### Example
<pre><code class="language-gherkin">
Page contains the text pattern "You have no messages to show"
The page contains text pattern "You have no messages to show"
The content of the page contains text pattern "You have no messages to show"

"messagesBox" contains the text pattern "You have no messages to show"
The "messagesBox" contains text pattern "You have no messages to show"
The content of the "messagesBox" contains text pattern "You have no messages to show"
 </code></pre>
