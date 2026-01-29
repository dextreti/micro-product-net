pipeline {
    agent {
        docker { 
            image 'mcr.microsoft.com/dotnet/sdk:10.0' 
            args '-u root'
        }
    }
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Restore') {
            steps {
                // El formato .slnx es nativo en .NET 10
                sh 'dotnet restore Catalog.slnx'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build Catalog.slnx --configuration Release --no-restore'
            }
        }
    }
}