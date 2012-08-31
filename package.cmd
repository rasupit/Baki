nuget.exe update -self

if not exist nuget mkdir nuget

pushd .
cd src\baki
nuget.exe pack -build -sym -prop Configuration=Release -o ..\..\nuget
popd