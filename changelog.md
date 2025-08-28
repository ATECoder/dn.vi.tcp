# Changelog
All notable changes to these libraries will be documented in this file.
The format is based on [Keep a Changelog].

[1.2.9371]: https://github.com/atecoder/dn.vi.tcp

## [1.2.9371] - 2025-08-28
- Update MSTest SDK to 3.10.3
- Use preview in net standard classes.
- use isr.cc as company name in the Serilog settings generator.
- Turn off source version in MS Test, Demo and Console projects.
- Remove incorrect Generate Assembly Version Attribute project settings.
- Use file version rather than product version when building the Product folder name because starting with .NET 8 the product version includes the source code commit information, which is not necessary for defining the product folder for settings and logging.

## [1.2.9251] - 2025-03-29
- Serilog Settings
  - Set log level to warning.
- Tests
  - Remove Console.WriteLine( $"@{methodFullName}" );
  - Replace with $"{methodFullName} initializing" );
  - Reduce reporting of test class initialization.
  - Output the name of the assembly under test.
- Fix incorrect new line escape character.

## [1.2.8933] - 2024-06-15 Preview 202406
* update editor config fixing code style and constants.
* apply code analysis rules.

## [1.2.8932] - 2024-06-14 Preview 202406
* Implement MS Test SDK.

## [1.2.8931] - 2024-06-14 Preview 202304
* Device.MSTest: Add a global suppression file.

## [1.2.8798] - 2023-02-02 Preview 202304
* Update to .NET 8.0.

## [1.1.8535] - 2023-05-15 Preview 202304
* Use cc.isr.Json.AppSettings.ViewModels project for settings I/O.

## [0.1.8518] - 2023-04-28 Preview 202304
* Split README.MD to attribution, cloning, open-source and read me files.
* Add code of conduct, contribution and security documents.
* Increment version.


## [0.1.8354] - 2022-11-14
* modify Keithley code for a single instrument.

&copy;  2022 Integrated Scientific Resources, Inc. All rights reserved.

[Keep a Changelog]: https://keepachangelog.com/en/1.0.0/
