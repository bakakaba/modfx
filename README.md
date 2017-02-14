# DotNetContainer

[![Run Status](https://img.shields.io/shippable/570320c12a8192902e1b9477/master.svg)](https://app.shippable.com/projects/570320c12a8192902e1b9477)

| Package     | Statistics |
| ----------- | ---------- |
| Extensions  | [![NuGet Badge](https://buildstats.info/nuget/DotnetContainer.Extensions)](https://www.nuget.org/packages/DotnetContainer.Extensions/) |
| Framework   | [![NuGet Badge](https://buildstats.info/nuget/DotnetContainer.Framework)](https://www.nuget.org/packages/DotnetContainer.Framework/) |
| Testing     | [![NuGet Badge](https://buildstats.info/nuget/DotnetContainer.Testing)](https://www.nuget.org/packages/DotnetContainer.Testing/) |
| Validation  | [![NuGet Badge](https://buildstats.info/nuget/DotnetContainer.Validation)](https://www.nuget.org/packages/DotnetContainer.Validation/) |

A base environment on which to build applications and services.

The DotNetContainer pulls together several libraries and ties them together to provide a consistent environment to target and build.

# Objectives
## Milestone 1

1. It should wire-up modules through module declarations and allow the usage of constructor dependency injection as its primary form of framework.

2. It shall be available as a console application or windows service to host its applications.

## Milestone 2

1. It should provide a generic testing environment to easily create tests.
