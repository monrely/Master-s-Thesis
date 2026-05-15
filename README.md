# MusicForge — Environment for Creating Music Using Programming

Master's thesis project at the Faculty of Mathematics, Physics and Informatics, Comenius University in Bratislava.

**Author:** Bc. Maksim Kulagin
**Supervisor:** doc. RNDr. Ľubomír Salanci, PhD.
**Department:** Department of Applied Informatics (FMFI.KAI)

## Description

The thesis belongs to the field of music programming and algorithmic music composition. It presents the design and implementation of a programming language and an accompanying environment for creating sounds and music. The text-oriented programming language, together with the environment, allows musical structures (patterns, tones, rhythm, and other parameters) to be described in the form of source code, which is then processed into an internal representation so that the result can be effectively played back. The work includes an overview of approaches to music creation using modern technologies, and an analysis of selected existing music programs, programming languages, and libraries. The research focuses on the usability of the language and environment within the target group, based on a set of model musical cases.

The system is built around a custom text-based domain-specific language. The language is processed by a hand-written compiler front-end consisting of a lexer, a recursive-descent parser, and a typed abstract syntax tree. A tree-walking interpreter transforms the AST into a list of timed musical events, which are played back through an audio backend. The graphical interface is implemented in WPF.

## Goal

The goal of the work is to design and implement a programming language and a suitable environment for the creation of sounds and music. The language and environment together enable musical structures to be described as source code and turned into audible output, while remaining usable for the intended target group on a set of model musical cases.

## Task Calendar

### Completed

| Period            | Task                                                                       |
| ----------------- | -------------------------------------------------------------------------- |
| Feb 20 – Feb 28   | Survey of existing music programming systems (SuperCollider, Sonic Pi, TidalCycles) |
| Feb 28 – Mar 8    | Reading on DSL design and live coding (see `Articles/`)                    |
| Mar 8 – Mar 18    | Language design — grammar, atom-based syntax, statement set                |
| Mar 18 – Mar 28   | Lexer implementation (`Compiler/Scanner.cs`)                               |
| Mar 28 – Apr 8    | Recursive-descent parser and typed AST (`Compiler/Parser.cs`, `Ast.cs`)    |
| Apr 8  – Apr 18   | Tree-walking interpreter and MIDI note parsing (`Compiler/Interpreter.cs`) |
| Apr 18 – Apr 28   | NAudio playback backend — sine synthesis, envelope, normalization          |
| Apr 28 – May 5    | WPF user interface and five example programs (`Examples/`)                 |
| May 5  – May 10   | First draft of the thesis text                                             |

### Planned

| Period          | Task                                                                  |
| --------------- | --------------------------------------------------------------------- |
| May – June      | Multiple instrument waveforms (square, triangle, sawtooth)            |
| May – June      | Real drum samples replacing the current noise-based placeholder       |
| Summer          | ADSR envelope replacing the current linear fade in / fade out         |
| Summer          | Chords and polyphony at the language level                            |
| Summer          | Parameters and variables in `define` blocks                           |
| Autumn          | Export to WAV and MIDI                                                |
| Autumn          | Line and column numbers in compiler error messages                    |
| Autumn          | Syntax highlighting in the WPF editor                                 |
| Before defence  | Usability study on a set of model musical cases                       |

## Repository Contents

- `Master's Thesis PDF.pdf` — the thesis text
- `Articles/` — reference papers used during research (see below)
- `Compiler/` — lexer, parser, AST, and interpreter for the MusicForge language
- `Audio/` — NAudio-based playback backend
- `Examples/` — sample `.mf` programs (scale, arpeggios, drums, layers, loop)
- `MainWindow.xaml`, `App.xaml`, `MusicForge.csproj`, `MusicForge.sln` — WPF application and project files

## References

The `Articles/` folder contains the papers studied during the research phase:

- **Diploma Articles.pdf** — overview article used for the diploma research
- **DSL + Compiler article.pdf** — background on domain-specific language design and compiler construction
- **Fernandez article.pdf** — additional reference on music-oriented language design
- **Sonic Pi article.pdf** — Aaron, S. and Blackwell, A. F. *From Sonic Pi to Overtone* (FARM 2013)
- **SuperCollider article.pdf** — McCartney, J. *Rethinking the Computer Music Language: SuperCollider* (Computer Music Journal, 2002)
- **Tidal article.pdf** — McLean, A. *Making Programming Languages to Dance to: Live Coding with Tidal* (FARM 2014)
