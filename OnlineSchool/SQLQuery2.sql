Select *from Subject
Select *from Result
Select Subject_ID from SUBJECT where SubjectName Like'%Math%'

Select *from RESULT where Subject_ID IN (  Select Subject_ID from SUBJECT where SubjectName Like'%Math%') AND Student_ID=3