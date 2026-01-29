pipeline {
    agent {
        docker { 
            image 'mcr.microsoft.com/dotnet/sdk:8.0' 
            args '-u root'
        }
    }
    environment {
        PROJECT_PATH = 'src/Order/Infrastructure/Adapters/Driving/Catalog.Order.API/Catalog.Order.API.csproj'
    }
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Restore') {
            steps {
                sh 'dotnet restore Catalog.slnx'
            }
        }
        stage('Build') {
            steps {
                sh "dotnet build Catalog.slnx --configuration Release --no-restore"
            }
        }
        stage('Test') {
            steps {
                // Si tienes proyectos de test, agrégalos aquí
                sh 'echo "Ejecutando tests..." '
            }
        }
        stage('Dockerize & Push') {
            when { branch 'main' }
            steps {
                // Aquí iría el docker build y push a tu Registry (Docker Hub/Harbor)
                echo 'Construyendo imagen Docker...'
            }
        }
    }
}