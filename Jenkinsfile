pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building C# project...'
                sh '/opt/homebrew/bin/dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                echo 'Running tests...'
                sh '/opt/homebrew/bin/dotnet test'
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