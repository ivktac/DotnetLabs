## TODO

- [x] Create interface `INameAndCopy` with property `Name` and method `object DeepCopy()`
- [x] Create new class `Team` and `ResearchTeam` that implement `Team` and `INameAndCopy` respectively
- [x] Update class `Person`, `Paper` that implement `INameAndCopy`

### Class `Person`

- [x] Override method `DeepCopy()` to return new instance of `Person` with same properties

### Class `Paper`

- [x] Override method `bool Equals(object obj)` to compare two papers by `Title` and `Author`
- [x] Add operator `==` and `!=` to compare two papers by `Title` and `Author`
- [x] Override method `int GetHashCode()` to return hash code of `Title` and `Author`
- [x] Override method `DeepCopy()` to return new instance of `Paper` with same properties

### Class `Team`

- [x] private field `string` for name of team
- [x] private field `int` for registration number of team
- [x] constructor with parameters `string` and `int`
- [x] constructor without parameters
- [x] property `string Name` for name of team
- [x] property `int RegistrationNumber` for registration number of team that should throw exception if value is less than 0
- [x] Override method `DeepCopy()` to return new instance of `Team` with same properties
- [x] Implement interface `INameAndCopy`
- [x] Override method `string ToString()` to return string with name of team and registration number of team
- [x] Override method `bool Equals(object obj)` to compare two teams by `Name` and `RegistrationNumber`

