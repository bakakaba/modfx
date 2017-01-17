#!/bin/bash

function restore {
	echo $'\n#### Restoring\n'
	dotnet restore
}

function build {
	echo $'\n#### Building src\n'
	get_src_projects | xargs dotnet build

	echo $'\n#### Building test\n'
	get_test_projects | xargs dotnet build
}

function test {
	echo $'\n#### Testing\n'
	for project in $(get_test_projects); do
		dotnet test $project -xml shippable/testresults/$(basename $project).xml
	done
}

function get_src_projects {
	find src -name 'project.json' -printf '%h\n'
}

function get_test_projects {
	find test -name 'project.json' -printf '%h\n'
}

restore
build
test
