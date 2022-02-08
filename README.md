# Dapper overview <!-- omit in toc -->

## Contents <!-- omit in toc -->

## What's Dapper?
- Dapper is a Micro-ORM (Object Relational Mapper).
- Micro-ORM maps database and .NET objects.
- ORM - Entity Framework.
- Micro-ORM - Dapper.
- Dapper is built by StackOverflow.

### Advantages of Micro-ORM
- Micro-ORM will usually perform better.
- Setting up full-blown ORM framework in your project might be too much.
- If application maker heavy use of stored procedures.
- Works with any database.
- You write your own sql query.

### Disadvantages of Micro-ORM
- Write your own SQL.
- Mappings can be difficult.
- Extensive validations.

## Orm or Micro-ORM?
- The perfect answer will be depends on the project.
- If your application is close to a current application with little complex calculations, I would lean more towards entity framework.
- But if that has a complex logic at the database and then Dapper or a combination of Dapper and Entity Framework would be my choice in that also I would lean more towards Dapper.
- But again, it depends on the project and the requirements.
- If efficiency is main concern, I will close my eyes and just go with micro overarm.