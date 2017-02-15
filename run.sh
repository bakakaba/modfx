#!/bin/bash

set -e

function clean {
    echo Cleaning...

    find src test -name obj -or -name bin | xargs -r rm -r
}

function restore {
    echo Restoring...

    dotnet restore
}

function build {
    echo Building...

    dotnet build --configuration Release
}

function test {
    echo Testing...

    for project in $(get_test_projects); do
        dotnet test $project
    done
}

function package {
    echo Packaging...

    if [ "$BRANCH" != "master" ]
    then
        local version_suffix="b$BUILD_NUMBER"
    fi

    for project in $(get_projects); do
        dotnet pack $project -c Release --version-suffix "$version_suffix" --include-symbols
    done

    echo "Produced the following packages:"
    find src -name "*.nupkg" -printf "    -> %p\n"
}

function publish {
    echo Publishing...

    for package in $(get_packages); do
        dotnet nuget push $package -s nuget.org -k "$NUGET_KEY"
    done
}

function get_projects {
    find src -name '*.csproj'
}

function get_test_projects {
    find test -name '*.csproj'
}

function get_packages {
    find src -name "*.nupkg" ! -name "*.symbols.nupkg"
}

case "$1" in
    clean)
        clean
        ;;
    restore)
        restore
        ;;
    build)
        build
        ;;
    test)
        test
        ;;
    package)
        clean
        restore
        test
        package
        ;;
    publish)
        publish
        ;;
    *)
        cat <<EOF
Usage: $0 [command]

Commands:

    clean       Removes all bin/ and obj/ folders.

    restore     Restores dependent packages for projects specified in the solution.

    build       Builds projects specified in the solution.

    test        Tests all projects under test/.

    package     Performs clean build, test and pack.

    publish     Publishes all packages under src/

    help        Prints this message.

EOF
        exit 1
        ;;
esac
