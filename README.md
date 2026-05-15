# MusicForge — Environment for Creating Music Using Programming

Master's thesis project at the Faculty of Mathematics, Physics and Informatics, Comenius University in Bratislava.

**Author:** Bc. Maksim Kulagin
**Supervisor:** doc. RNDr. Ľubomír Salanci, PhD.
**Department:** Department of Applied Informatics (FMFI.KAI)

## Description

The thesis belongs to the field of music programming and algorithmic music composition. It presents the design and implementation of a programming language and an accompanying environment for creating sounds and music. The text-oriented programming language, together with the environment, allows musical structures (patterns, tones, rhythm, and other parameters) to be described in the form of source code, which is then processed into an internal representation so that the result can be effectively played back. The work includes an overview of approaches to music creation using modern technologies, and an analysis of selected existing music programs, programming languages, and libraries.

The system is built around a custom text-based domain-specific language. The language is processed by a hand-written compiler front-end consisting of a lexer, a recursive-descent parser, and a typed abstract syntax tree. A tree-walking interpreter transforms the AST into a list of timed musical events, which are played back through an audio backend. The graphical interface is implemented in WPF.

## Goal

The goal of the work is to design and implement a programming language and a suitable environment for the creation of sounds and music. The language and environment together enable musical structures to be described as source code and turned into audible output.

## Task Calendar

### Completed

| Period            | Task                                                                       |
| ----------------- | -------------------------------------------------------------------------- |
| Feb 20 – Feb 28   | Survey of existing music programming systems (SuperCollider, Sonic Pi, TidalCycles) |
| Feb 28 – Mar 8    | Reading on DSL design                   |
| Mar 8 – Mar 28    | Learning compilator and interpretator programming              |
| Mar 28 – Apr 8    | Creating of Lexer  (`Compiler/Scanner.cs`)                               |
| Mar 28 – Apr 8    | Creating of AST (`Parser.cs`, `Ast.cs`)    |
| Apr 8  – Apr 18   | Tree-walking interpreter and MIDI note parsing (`Compiler/Interpreter.cs`) |
| Apr 18 – Apr 28   | NAudio playback backend — sine synthesis, envelope, normalization          |
| Apr 28 – May 10   | Bugs fixing and optimization                  |
| Apr 28 – May 10   | Thesis LaTeX documentation                                             |

### Planned

| Task                                                                  |
| --------------------------------------------------------------------- |
| Multiple instrument waveforms (square, triangle, sawtooth)            |
| Real drum samples replacing the current noise-based placeholder       |
| ADSR envelope replacing the current linear fade in / fade out         |
| Chords and polyphony at the language level                            |
| Parameters and variables in `define` blocks                           |
| Export to WAV and MIDI                                                |
| Line and column numbers in compiler error messages                    |
| Syntax highlighting in the WPF editor                                 |
| Usability study on a set of model musical cases                       |

 
 
