# tw.edu.ltu.dcd.unity.net.restful
RESTful 外掛
## Install 
Windows -> Package Manager
Add package from git URL...
```
https://github.com/dcd-ltu/tw.edu.ltu.dcd.unity.net.restful.git
```
## Usage
```csharp=
//Product.cs is entiy
Product product = RESTful.Get<Product>(url);
```