//------------------------------------------------------------------------------//
//---------------Types used to represent student-project allocation data--------//
//------------------------------------------------------------------------------//
module EETypes


// ------------------------------------------------
/// unique id for person equal to CID in database
type CID = string
// DU
type CIDdu = CIDdu of string
// ------------------------------------------------



// ------------------------------------------------
/// "Autumn" or "Spring"
type TermType = string

/// All possible TermType values
let termTypeList = ["Autumn";"Spring"]
// DU version
type TermTypeDU = | Autumn | Spring

let termTypeToDU x : TermTypeDU =
    match x with
    | "Autumn" ->   Autumn
    | "Spring" ->   Spring 
    | _ -> failwithf "Invalid term type input: '%s'. Input must be either 'Autumn' or 'Spring'" x
// ------------------------------------------------



// ------------------------------------------------
/// Data describes the year and course of a student.
/// Only 3rd and 4th year students are considered.
/// 'E' (EEE) or 'I' (EIE) followed by '3' or '4'. eg "E3"
type ERegYr = string
/// All possible ERegYr values
let eRegYrList = ["E3";"I3";"E4";"I4"]
// DU version
type ERegYrDU = | E3 | I3 | E4 | I4

let ERegYrToDU (eRegYr:ERegYr) =
    match eRegYr with
    | "E3" -> E3
    | "I3" -> I3
    | "E4" -> E4
    | "I4" -> I4
    | _ -> failwithf $"Invalid ERegYr: {eRegYr}"

let ERegYrFromDU du: ERegYr = 
    match du with
    | E3 -> "E3"
    | E4 -> "E4"
    | I3 -> "I3"
    | I4 -> "I4"
    
/// Extract year from ERegYr
let yearOfReg (regYr: ERegYr) = 
    match regYr with
    | "E3" -> 3
    | "I3" -> 3
    | "E4" -> 4
    | "I4" -> 4
    | _ -> failwithf $"Invalid ERegYr: {regYr}"

let yearOfRegDU (regYr:ERegYrDU) =
    match regYr with
    | E3 | I3 -> 3
    | E4 | I4 -> 4 


/// Extract course from ERegYr
let courseOfReg (regYr: ERegYr) = 
    match regYr with
    | "E3" -> "EEE"
    | "I3" -> "EIE"
    | "E4" -> "EEE"
    | "I4" -> "EIE"
    | _ -> failwithf $"Invalid ERegYr: {regYr}"

let courseOfRegDU (regYr:ERegYrDU) =
    match regYr with
    | E3 | E4 -> "EEE"
    | I3 | I4 -> "EIE" 
// ------------------------------------------------



// ------------------------------------------------
/// Information describing one student
type Person = {
    /// name of person - first name followed by family name
    Name: {|First: string; Last: string|}
    CID: CID
    ERegYr: ERegYr
    CourseMarkSoFar: float
}

type PersonDU = {
    /// name of person - first name followed by family name
    Name: {|First: string; Last: string|}
    CID: CIDdu
    ERegYr: ERegYrDU
    CourseMarkSoFar: float
}

with 
    /// Override ToString() method to return a more readable string representation of the person
    /// eg "Theresa Alexander (12345678) E3".
    /// This makes the formatted print of a person from printf more readable than the default record representation.
    override this.ToString() = $"{this.Name.First} {this.Name.Last} ({this.CID}) {this.ERegYr}"
    /// Convenience property to get year
    member this.YearOf = this.ERegYr |> yearOfRegDU
    /// Convenience property to get course
    member this.CourseOf = this.ERegYr |> courseOfRegDU
// ------------------------------------------------




// ------------------------------------------------
/// Module code eg "Elec60015"
type ModuleCode = string
// DU
type ModuleCodeDU = ModuleCodeDU of string
// ------------------------------------------------



// ------------------------------------------------
/// Information describing which modules a student is doing
type ModuleOptions = {
    CID: CID
    Options: ModuleCode list
}

type ModuleOptionsDU = {
    CID: CIDdu
    Options: ModuleCodeDU list
}
// ------------------------------------------------




// ------------------------------------------------
/// Information describing one module
type Module = {
    ModuleCode: ModuleCode
    OptionalFor: ERegYr list
    Term: TermType
}
with
    /// Override ToString() method to return a more readable string representation of the module
    /// eg "Elec60015 (Spring) OptionalFor: E3, I3".
    /// This makes the formatted print of a module from printf more readable than the default record representation.
    override this.ToString() = $"""{this.ModuleCode} ({this.Term}) OptionalFor: {String.concat ", " this.OptionalFor}"""


type ModuleDU = {
    ModuleCode: ModuleCodeDU
    OptionalFor: ERegYrDU list
    Term: TermTypeDU
}
with

    /// Override ToString() method to return a more readable string representation of the module
    /// eg "Elec60015 (Spring) OptionalFor: E3, I3".
    /// This makes the formatted print of a module from printf more readable than the default record representation.
    override this.ToString() = $"""{this.ModuleCode} ({this.Term}) OptionalFor: {String.concat ", " (List.map ERegYrFromDU this.OptionalFor)}"""
// ------------------------------------------------



// ------------------------------------------------
/// Immutable data for EEE database
/// The maps are updated immutably when sql server data is read and processed.
/// the returned EEData is then immutable and has everything needed to do the allocation.
type EEData = {
    /// get person data from CID
    People: Map<CID,Person>
    /// get Curriculum Table record by CourseCode
    Curriculum: Map<ModuleCode,Module>
    Options: Map<CID,ModuleOptions>
}

type EEDataDU = {
    /// get person data from CID
    People: Map<CIDdu,PersonDU>
    /// get Curriculum Table record by CourseCode
    Curriculum: Map<ModuleCodeDU,ModuleDU>
    Options: Map<CIDdu,ModuleOptionsDU>
}
// ------------------------------------------------




/// List of all modules
let (moduleList':Module list) = [
    { ModuleCode = "Elec60015"; OptionalFor = ["E3";"I3"]; Term = "Spring"}
    { ModuleCode = "Elec60016"; OptionalFor = ["E3";"I3"]; Term = "Spring"}
    { ModuleCode = "Elec60017"; OptionalFor = ["E3"]; Term = "Spring"}
    { ModuleCode = "Elec60018"; OptionalFor = ["I3"]; Term = "Spring"}
    { ModuleCode = "Elec60019"; OptionalFor = ["E3"]; Term = "Autumn"}
    { ModuleCode = "Elec60020"; OptionalFor = ["E3";"I3"]; Term = "Autumn"}
    { ModuleCode = "Elec60021"; OptionalFor = ["E3";"I3"]; Term = "Autumn"}
    { ModuleCode = "Elec60022"; OptionalFor = ["I3"]; Term = "Autumn"}
    { ModuleCode = "Elec70001"; OptionalFor = ["E4";"I4"]; Term = "Spring"}
    { ModuleCode = "Elec70002"; OptionalFor = ["E4";"I4"]; Term = "Spring"}
    { ModuleCode = "Elec70003"; OptionalFor = ["E4"]; Term = "Spring"}
    { ModuleCode = "Elec70004"; OptionalFor = ["I4"]; Term = "Spring"}
    { ModuleCode = "Elec70005"; OptionalFor = ["E4";"I4"]; Term = "Autumn"}
    { ModuleCode = "Elec70006"; OptionalFor = ["E4";"I4"]; Term = "Autumn"}
    { ModuleCode = "Elec70007"; OptionalFor = ["E4"]; Term = "Autumn"}
    { ModuleCode = "Elec70007"; OptionalFor = ["I4"]; Term = "Autumn"}
]

// functions to help map moduleList to moduleListDU
let ModuleToModuleDU (m:Module) : ModuleDU =
    {
        ModuleCode= ModuleCodeDU m.ModuleCode;
        OptionalFor= List.map ERegYrToDU m.OptionalFor ;
        Term= termTypeToDU m.Term
    }


let moduleList = List.map ModuleToModuleDU moduleList'

let moduleGroupSizes =
    moduleList
    |> List.map (fun m -> m.ModuleCode, (System.Random 0).Next(3, 9))
    |> Map.ofList
// ------------------------------------------------