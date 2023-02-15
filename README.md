## Projeto Testes para HyperAtiva de Gustavo Fernandes

######################
## Passos para Instalacao do Projeto

## Passo 1:
## Rodar o Script de banco de dados anexado em SqlServer - createdDataBase.sql

## Passo 2:
## Neste pacote sera criado o Usuario com o Email - teste@teste.com
## Este Usuario "teste@teste.com" sera usado para se gerar o Token no endpoint - (GET) api/v1/user/token/{email}

## Passo 3:
## Com o Token gerado neste endpoint entrar no Authorize, direto com ele, para acessar os demais endpoints
######################

######################
## Endpoints para cartao de crediro 

## Criacao de Cartao de Credito pelo Usuario
## (POST) api/v1/creditCard/insertByUser/{creditCard}
## Sera devolvido a mensagem se o Cartao de Credito foi criado, ou se jah existe no banco de dados

## Criacao de Cartao de Credito por arquivo
## (POST) api/v1/creditCard/insertByFile - parametros file (arquivo segue arquivo anexado - "DESAFIO-HYPERATIVA.txt")
## Sera devolvido a mensagem se o Cartao de Credito foi criado, ou se jah existe no banco de dados

## Verificacao se Cartao de Credito jah existe ou nao no sistema
## (GET) api/v1/creditCard/verify/{creditCard}
## Sera devolvido a mensagem se o Cartao de Credito se jah existe ou nao existe no banco de dados
######################

######################
## Tabelas de banco de dados

## tbUsers
## Tabela de Usuarios - utilizado para geracao se Tokens

## tbFiles
## Tabela de Arquivos - dados do arquivo

## tbCreditCards
## Tabela de Cartoes de Credito - dados de Cartoes de Credito

## tbLog
## Tabela de Logs - dados de Log do Sistema
######################