# ASP.NET 5 Buildpack

This is a [Heroku buildpack](http://devcenter.heroku.com/articles/buildpack) for building [ASP.NET 5](http://www.asp.net/vnext/overview/aspnet-vnext/aspnet-5-overview) apps using [`project.json` files](https://github.com/aspnet/Home/wiki/Project.json-file) and the [DNX Utility](https://github.com/aspnet/Home/wiki/DNX-utility).

[Mono](http://www.mono-project.com/) is bundled for runtime execution.

## Usage

Example usage:

    $ heroku create --buildpack http://github.com/heroku/dotnet-buildpack.git
    $ git push heroku master

The buildpack will detect your app as ASP.NET 5 if it has `project.json`. If the source code you want to build contains multiple `project.json` files, you can use a [`.deployment`](https://github.com/projectkudu/kudu/wiki/Customizing-deployments) or set a `$PROJECT` config var to control which one is built.


## Notice
1. The default DNX Version is an unstable version. So if you want to use a stable one, you should set "stable" to true in `.deployment`.
2. If you developed a project using VS, don't forget to add the `Kestrel` dependence in `project.json`.
