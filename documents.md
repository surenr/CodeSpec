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
3. [Click](#click)
    1. [Click on](#click-on)
    2. [Click and wait](#click-and-wait)
4. [Drag and drop](#drag-and-drop)
5. [Enter value](#click-on)
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

### Parameters  
`<element key>` - identifier defined in the repository which  represents a web element

`<expected value>` - the value expected to be there

### Example

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

###Alternatives

The content of the page contains text pattern  `<text pattern>`

`<element key>` contains the text pattern "You have no messages to show"

The content of the `<element key>` contains text pattern `<text pattern>`

### Parameters  

`<text pattern>` - the value expected to be there

`<element key>` - identifier defined in the repository which  represents a web element

### Example
<pre><code class="language-gherkin">
Page contains the text pattern "You have no messages to show"
The page contains text pattern "You have no messages to show"
The content of the page contains text pattern "You have no messages to show"

"messagesBox" contains the text pattern "You have no messages to show"
The "messagesBox" contains text pattern "You have no messages to show"
The content of the "messagesBox" contains text pattern "You have no messages to show"
 </code></pre>

---

#3. Click

##3.1 Click on

###Grammer
I click on `<element key>`

I click on `<element key>` with the `<selector type>` of `<selector value>`

### Parameters  

`<element key>` - identifier defined in the repository which  represents a web element

`<selector type>` - Type of the element selector ( either 'id' or 'xpath')

`<selector value>` - value of the element selector

### Example
<pre><code class="language-gherkin">
I click on "submitButton"
Click on "submitButton"

I click on element "submitButton" with the "id" of "submitButton"
</code></pre>




##3.2 Click and wait

###Grammer
I click on `<element key>` and wait `<no of seconds>` seconds

I click on element `<element key>` with the `<selector type>` of `<selector value>` and wait `<no of seconds>` seconds

### Parameters  

`<element key>` - identifier defined in the repository which  represents a web element

`<no of seconds>` - How many seconds to wait

`<selector type>` - Type of the element selector ( either 'id' or 'xpath')

`<selector value>` - value of the element selector


### Example
<pre><code class="language-gherkin">
I click on "submitButton" and wait "30" seconds
Click on "submitButton" and wait "30" seconds

I click on element "submitButton" with the "id" of "submitButton" and wait "30" seconds
Click on element "submitButton" with the "id" of "submitButton" and wait "30" seconds
 </code></pre>

 ---

#4. Drag and drop

Drag `<element1 key>` and drop on to `<element2 key>`

###Grammer
I drag `<element1 key>` and drop on to `<element2 key>`

###Alternatives

I drag `<element1 key>` and drop on to `<element2 key>`

I drag `<element1 key>` with the `<selector type>` of `<selector value>` and drop on to `<element2 key>` with the `<selector type>` of `<selector value>`

### Parameters  

`<element(n) key>` - identifier defined in the repository which  represents a web element

`<selector type>` - Type of the element selector ( either 'id' or 'xpath')

`<selector value>` - value of the element selector

### Example
<pre><code class="language-gherkin">
I drag "widget1" and drop on to "dashboard"
drag "widget1" and drop on to "dashboard"

I drag "widget1" with the "id" of "widget1" and drop on to "dashboard" with the "id" of "dashboard"
 </code></pre>

 ---

#5. Enter value

Enter `<value>` to `<element1 key>`

###Grammer

I enter `<value>` to the `<element1 key>`

###Alternatives

I enter value `<value>` to the `<element1 key>` with the `<selector type>` of `<selector value>`

### Parameters  

`<value>` - the value  to be put

`<element key>` - identifier defined in the repository which  represents a web element

`<selector type>` - Type of the element selector ( either 'id' or 'xpath')

`<selector value>` - value of the element selector

### Example
<pre><code class="language-gherkin">
I enter "Sri Lanka" to the "countryField"

I enter value "Sri Lanka" to the "countryField" with the "id" of "countryTextField"

</code></pre>

---
