
# Building

```
dotnet build
```

# Running

```
dotnet run --project alcdel
```

# Expected output

```
the path to lib.dll is '/private/tmp/alcdel/alcdel/bin/Debug/netcoreapp3.0/subs'
trying to find lib, Culture=neutral, PublicKeyToken=null in '/private/tmp/alcdel/alcdel/bin/Debug/netcoreapp3.0/subs/lib.dll'
Created a1
Delegating load of lib2, Culture=neutral, PublicKeyToken=null
trying to find lib2, Culture=neutral, PublicKeyToken=null in '/private/tmp/alcdel/alcdel/bin/Debug/netcoreapp3.0/subs/lib2.dll'
Returned from delegating load of lib2, Culture=neutral, PublicKeyToken=null
a0 same as a2 ? True
a1 same as a2 ? False
a2 same as default? False
```
