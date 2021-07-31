# generic-logger (Generic Logger for .Net Core projects)

Guess that you have a big system and you have implemented logging using a 3rd party library like nLog. You have implemented logging at everywhere in your system in almost 10 project libraries and 100s of classes. Now for any specific reason you must need to change from nLog to Serilog or log4net or Microsoft Logging, you will have to change code in 100s of classes and references in all project libraries, which could be a very tedious job.

# What could be a solution?
This generic-logger could be a solution to above problem.

# How?
- Create your own project library which could have few functions to log Debug, Information, Warning or Error or few others could be there like FatalError or CriticalError.
- Now give reference of that project in all other your 10 projects and use functions of your own library in all 100s of classes to log.
- In this case if you need to change 3rd party library in any case then you will have to change into your own generic-logger.
