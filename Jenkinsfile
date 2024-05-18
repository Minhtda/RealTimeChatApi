pipeline{
  agent any 
     tools {
           dotnetsdk "7.0" // Name: 5.0, URL: (download URL for .NET SDK 5.0)
           }
      stages {
        stage('Checkout'){
          steps{
             git branch: 'main', credentialsId: 'ac83972b-6e89-455b-8b18-bf5eb62afcb6', url: 'https://github.com/Goods-Exchange/BackendAPIProject.git'
          }
         
        }
         stage('Restore'){
                  steps {
                        withDotNet(sdk:'7.0'){
                            dotnetRestore project: 'BackendAPI.sln'
                        }
                    }
    }
     }
      post {
           success {
             echo 'Pull code from git server success'
                }
      }
   
}
