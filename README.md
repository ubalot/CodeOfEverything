# Code of everything

---

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

Extract media files from Word files
```bash
dotnet run extract --type media --from <path_to_docx>
```

---

Format code file (empty line at the end; no trailing spaces at the end of line)
```bash
dotnet run format --file <path_to_file>
```
