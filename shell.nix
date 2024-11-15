let
  pkgs = import <nixpkgs> {};
in pkgs.mkShell {
  packages = [
    (with pkgs; [
      dotnet-sdk_8
    ])
  ];
}
