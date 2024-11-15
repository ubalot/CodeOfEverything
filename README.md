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
- [Cipher](Services/Tasks/Cipher/README.md)
- [Extractor](Services/Tasks/Extractor/README.md)
- [Source code file formatter](Services/Tasks/SourceCodeFormatter/README.md)
- [System info](Services/Tasks/SystemInfo/README.md)