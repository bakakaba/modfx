FROM microsoft/aspnet

COPY . /working
WORKDIR /working

RUN ["dnu", "restore"]
#RUN ["dnu", "build"]