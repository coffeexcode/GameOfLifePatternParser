# GameOfLifePatternParser
Generate pattern build code for Conway's Game of Life.

This is a work in progress. The process is very rough at the current time

## Process
* Create input file containing two characters (an alive character and a dead character). Ensure the file is a square (rows * colums).
* Run GameOfLifePatternParser /path/to/input "aliveChar" variableName outputValue
* Code is generated at /path/to/code_input

At this time, code can be put in the build method for any patterns.

## To do
* Increase robustness of generated code
* Generate entire class structure
* Tests & code review
