module Tick2
open LibExtensions
open EETypes

// Assessment of Tick 2 will be 100% if passed.
// To pass the tick > 2 of Parts A - D must be correctly implemented.
// Part E should be attempted by students who have completed Parts A-D and have time to spare. Attempting this 
// part even without success will mean the solution, when you get it, will be of more use to you.
// Part X1 and X2 are for reflection only and must be discussed in the interview, but are not otherwise assessed.
// Students competent in material taught in weeks 1-4 should be able to complete all of Parts A-D of this tick.

//-------------------Answers should be written in this file replacing failwithf statements by implementations--------------//
//-------------------Any number of additional helper functions may be defined at top level in this file--------------------//
//-------------------Add code to run and test your functions in Program.fs-------------------------------------------------//
//-------------------Rigorous testing is not required for this Tick--------------------------------------------------------//

// Part A - implement the function below

/// Generate a list of random module codes for a student obeying the rules:
/// - The student must take three modules.
/// - The student registration must be in the OptionalFor list of each taken module.
/// - The student must not take the same module twice.
/// - You must use StudentGen.randomStudentOptions, and pass it rng.
let studentOptionsPartA (rng: System.Random) (person: Person) : ModuleOptions =
    failwithf "Not Implemented"
