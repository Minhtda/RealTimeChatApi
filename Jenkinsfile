pipeline{
  agent any 
     tools {
           dotnetsdk "7.0" 
           }
      stages {
        stage('Checkout'){
          steps{
             git branch: 'main', credentialsId: 'ac83972b-6e89-455b-8b18-bf5eb62afcb6', url: 'https://github.com/Goods-Exchange/BackendAPIProject.git'
          }
        }
         stage('Restore solution'){
                  steps {
                        withDotNet(sdk:'7.0'){
                            dotnetRestore project: 'BackendAPI.sln'
                        }
                    }
              }  
        stage('Build solution') {
           steps {
              withDotNet(sdk: '7.0') { // Reference the tool by ID
               dotnetBuild project: 'BackendAPI.sln', sdk: '7.0', showSdkInfo: true, unstableIfErrors: true, unstableIfWarnings: true,noRestore: false
             }
             }
            }
          stage('Test solution'){
            steps {
              withDotNet(sdk:'7.0'){
                dotnetTest noBuild: true, project: 'BackendAPI.sln', sdk: '7.0',resultsDirectory: 'Test/CoverageReport',collect: 'XPlat Code Coverage'
              }
            }
          }
          stage('Publish HTML'){
            steps{
              publishHTML([allowMissing: false, alwaysLinkToLastBuild: false, keepAll: false, reportDir: 'Test/CoverageReport', reportFiles: 'index.html', reportName: 'HTML Report', reportTitles: '', useWrapperFileDirectly: true])
            }
          }
         }
      post {
           success {
             echo 'Pull code from git server success'
                }
      }
   
}
