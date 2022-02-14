# Dapper overview <!-- omit in toc -->

## Contents <!-- omit in toc -->

- [1. What's Dapper?](#1-whats-dapper)
- [2. Package dependencies](#2-package-dependencies)
- [3. Advantages and disadvantages](#3-advantages-and-disadvantages)
  - [3.1. Advantages of Micro-ORM](#31-advantages-of-micro-orm)
  - [3.2. Disadvantages of Micro-ORM](#32-disadvantages-of-micro-orm)
- [4. Orm or Micro-ORM?](#4-orm-or-micro-orm)

## 1. What's Dapper?
- Dapper is a Micro-ORM (Object Relational Mapper).
- Micro-ORM maps database and .NET objects.
- ORM - Entity Framework.
- Micro-ORM - Dapper.
- Dapper is built by StackOverflow.

## 2. Package dependencies
- dotnet add package Dapper --version X.X.X
- dotnet add package Dapper.Contrib --version X.X.X

## 3. Advantages and disadvantages
### 3.1. Advantages of Micro-ORM
- Micro-ORM will usually perform better.
- Setting up full-blown ORM framework in your project might be too much.
- If application maker heavy use of stored procedures.
- Works with any database.
- You write your own sql query.

### 3.2. Disadvantages of Micro-ORM
- Write your own SQL.
- Mappings can be difficult.
- Extensive validations.

## 4. Orm or Micro-ORM?
- The perfect answer will be depends on the project.
- If your application is close to a current application with little complex calculations, I would lean more towards entity framework.
- But if that has a complex logic at the database and then Dapper or a combination of Dapper and Entity Framework would be my choice in that also I would lean more towards Dapper.
- But again, it depends on the project and the requirements.
- If efficiency is main concern, I will close my eyes and just go with micro overarm.