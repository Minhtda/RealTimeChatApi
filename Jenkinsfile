pipeline{
  agent any 
      stages {
        stage('Checkout'){
          git branch: 'main', credentialsId: 'ac83972b-6e89-455b-8b18-bf5eb62afcb6', url: 'https://github.com/Goods-Exchange/BackendAPIProject.git'
        }
     }
      post {
            success {
                   echo 'Pull code success'
                }
      }
}
