pipeline {
    agent {
        docker { 
            image 'mcr.microsoft.com/dotnet/sdk:10.0' 
            args '-u root -v /var/run/docker.sock:/var/run/docker.sock' 
        }
    }
    environment {
        IMAGE_NAME = "dextreti/order-api:latest"
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Restore & Build') {
            steps {
                
                sh 'dotnet restore Catalog.slnx'
                sh 'dotnet build Catalog.slnx --configuration Release --no-restore'
            }
        }
        stage('Docker Build') {
            steps {
                
                sh "docker build -t ${IMAGE_NAME} ."
            }
        }
        stage('Docker Tag & Push') {
            steps {
                echo "Imagen ${IMAGE_NAME} lista localmente en Debian."                
            }
        }
    }
}