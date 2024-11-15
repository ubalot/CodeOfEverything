# Code of everything

## Build
```bash
dotnet publish -c release --self-contained --runtime linux-x64
```

---

## Develop
### Run the project
```bash
dotnet run -- --help
```

---

Commands:
- [Cipher](src/Commands/Cipher/README.md)
- [Extractor](src/Commands/Extractor/README.md)
- [Source code file formatter](src/Commands/SourceCodeFormatter/README.md)
- [System info](src/Commands/SystemInfo/README.md)