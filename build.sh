#!/bin/bash

function test {
	echo $'\n### Testing\n'
	for project in $(get_test_projects); do
		dotnet test $project
	done
}

function get_test_projects {
	find test -name '*.csproj'
}

dotnet restore
dotnet build
test
