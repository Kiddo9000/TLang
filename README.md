# TLang
TLang is my crappy C# programming language and compiler. It's really basic and it sucks. Don't use this for actual applications. This is just for fun. Feel free to break it, though.  
  
## Using  
You can download the pre-compiled version in releases.  
To compile a file, pass the file's path as an argument. The executable will be generated in the same folder with the same name.  
Example: `TLang.exe "C:\Some Folder\Some File.tl"`  
  
## Programming Reference  
Here is a quick reference for all of the stuff you can use in your program.  
  
## Instructions
Instruction | Parameters | Description
--- | --- | ---
. | Console | . 
*CLS* | (void) | Clears the console.
*WRO* | (string) Input, (bool) Newline | Writes text to the console.
*WRI* | (string) OutVariable | Asks the user for input and writes the input to the variable specified.
*WRV* | (string) Variable, (bool) Newline | Writes the variable contents to the console.
*WRN* | (void) | Writes a blank line to the console.
*WFK* | (void) | Waits for keyboard input.
. | Variables | .
*DEF* | (string) Name, (string) Value | Makes a new variable with the provided name and value.
*SET* | (string) Variable, (string) Value | Updates the specified variable's value.
*DES* | (string) Variable | Removes the specified variable from memory.
. | Conditional Functions | .
*VEQ* | (string) Variable, (string) Value, (string) OutVariable | Checks if the specified variable's contents are equal to the provided value and writes either TRUE or FALSE to the output variable.
*VCT* | (string) Variable, (string) Value, (string) OutVariable | Checks if the specified variable's contents contain the provided value and writes either TRUE or FALSE to the output variable.
. | Misc Functions | .
*SLP* | (string) Time | Sleeps for the amount of time specified in milliseconds.
*QUT* | (void) | Quits the program.
  
### Sections
You can make a section by writing the name of the section, followed by a colon.  
Example: `MySection:`  
  
*Note: Sections can only contain letters a-z. No numbers or special characters are allowed.*  
  
You can go to a section by typing the section name with a leading period.  
Example: `.MySection`  
  
A complete example:  
```
.SectionB

SectionA:
	WRO Some stuff in section A!||TRUE
	QUT

SectionB:
	WRo Some stuff in section B! Let's go to section A!||TRUE
	.SectionA
```  
  
Output from example:  
```
Some stuff in section B! Let's go to section A!
Some stuff in section A!
```  
  
### Conditions
You can go to a specific section if a variable is equal to TRUE.  
Example: `$MyVar MySection`  
  
A breakdown of that code:  
```
$         <- Indicates this is a condition statement.
MyVar     <- The variable to read.
MySection <- The section to go to if the variable is equal to TRUE.
```  
  
### Parameters
To define multiple parameters in an instruction, you can use the `||` delimiter.  
Example: `WRO Some Text I want to write||TRUE`  
  
### Booleans
Booleans are defined with either `TRUE` or `FALSE`. That's it. I don't know what you expected to find here.  
  