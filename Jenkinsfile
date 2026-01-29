pipeline {
    agent none
    
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
            agent any 
            steps {
                
                sh "docker build -t ${IMAGE_NAME} ."
                echo "Imagen construida. kube, ejecuta 'minikube image load ${IMAGE_NAME}' manualmente."
            }
        }

        stage('Docker Tag & Push') {
            agent any
            steps {
                
                echo "Imagen ${IMAGE_NAME} cargada directamente en el clúster de Kubernetes."
            }
        }
    }
}