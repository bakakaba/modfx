#!/bin/bash

set -e

function clean {
    echo Cleaning...

    find src test -iname obj -or -iname bin | xargs -r rm -r
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
        dotnet pack $project --configuration Release --version-suffix "$version_suffix"
    done
}

function publish {
    echo Publishing...

    if [ "$USER" == "shippable" ]
    then
        find src -iname "*.nupkg" | xargs dotnet nuget push
    else
        echo "Not run by CI. Skipping packages:"
        find src -iname "*.nupkg" -printf "    -> %p\n"
    fi
}

function get_projects {
    find src -name '*.csproj'
}

function get_test_projects {
    find test -name '*.csproj'
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
    publish)
        clean
        restore
        test
        package
        publish
        ;;
    *)
        cat <<EOF
Usage: $0 [command]

Commands:

    clean       Removes all bin/ and obj/ folders.

    restore     Restores dependent packages for projects specified in the solution.

    build       Builds projects specified in the solution.

    test        Tests all projects under test/

    publish     Performs a clean build and test before
                publishing all packages under src/

    help        Prints this message.

EOF
        exit 1
        ;;
esac
