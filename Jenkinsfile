pipeline {
    agent none
    environment {        
        IMAGE_NAME = "dextre78/order-api:latest"
    }
    stages {
        stage('Restore & Build') {
            agent { docker { image 'mcr.microsoft.com/dotnet/sdk:10.0'; args '-u root' } }
            steps {
                checkout scm
                sh 'dotnet restore Catalog.slnx'
                sh 'dotnet build Catalog.slnx --configuration Release --no-restore'
            }
        }

        stage('Docker Build & Push') {
            agent any 
            steps {
                // Construimos la imagen localmente
                sh "docker build -t ${IMAGE_NAME} ."
                
                // Usamos las credenciales guardadas en Jenkins para subirla a la nube
                withCredentials([usernamePassword(credentialsId: 'docker-hub-credentials', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
                    sh "echo \$DOCKER_PASS | docker login -u \$DOCKER_USER --password-stdin"
                    sh "docker push ${IMAGE_NAME}"
                }
            }
        }

        stage('Deploy to K8s') {
            agent any
            steps {                
                sh "kubectl apply -f deployment.yaml"
            }
        }
        
    }
}