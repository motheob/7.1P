pipeline {
    agent any

    environment {
        // Manually concatenate PATH with the additional directory
        PATH = "/usr/local/share/dotnet:${env.PATH}"
    }

    stages {
        stage('Test Shell') {
            steps {
                echo 'Testing shell...'
                sh 'echo "Shell is working"'
            }
        }

        stage('Build') {
            steps {
                echo 'Building C# project...'
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                echo 'Running tests...'
                sh 'dotnet test'
            }
        }

        stage('Build Docker Image') {
            steps {
                echo 'Building Docker image...'
                sh 'docker build -t mycsharpapp:latest .'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploying Docker container...'
                sh 'docker run -d -p 8080:80 mycsharpapp:latest'
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully'
        }
        failure {
            echo 'Pipeline failed'
        }
    }
}
