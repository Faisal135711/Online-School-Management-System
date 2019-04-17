Select *from CLASSROUTINE
SElect *from STUDENT

			SElect *from CLASSROUTINE where Class_ID IN (Select CLASS_Id From STUDENT where Student_ID=1) and Section_ID IN(Select Section_ID from STUDENT where Student_ID=1)