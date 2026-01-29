pipeline {
    agent none // No definimos un agente global
    
    environment {
        IMAGE_NAME = "dextreti/order-api:latest"
    }
    
    stages {
        stage('Restore & Build') {
            agent {
                docker { 
                    image 'mcr.microsoft.com/dotnet/sdk:10.0' 
                    args '-u root'
                }
            }
            steps {
                checkout scm
                sh 'dotnet restore Catalog.slnx'
                sh 'dotnet build Catalog.slnx --configuration Release --no-restore'
            }
        }

        stage('Docker Build') {
            agent any // Volvemos al entorno de Jenkins donde SÍ hay comando docker
            steps {
                // Aquí el comando docker funcionará porque usa el del host Debian
                sh "docker build -t ${IMAGE_NAME} ."
            }
        }

        stage('Docker Tag & Push') {
            agent any
            steps {
                echo "Imagen ${IMAGE_NAME} creada exitosamente en el host."
            }
        }
    }
}