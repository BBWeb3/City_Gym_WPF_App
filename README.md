# City_Gym_WPF_App
City_Gym_WPF_App_BIT502_A3

The following are the details of the project brief that outlines the scope and nature of this repository:

BIT502 Fundamentals of Programming
Assessment 3 Appendix A: Case Study

City Gym are looking to enhance their online membership form with a multi-page database
application that allows for easy storage and retrieval of member information. It will expand on
the online membership form you created in Assignment 2, to also include a fitness class
booking screen. After consulting with the client, you have taken the following notes:

• The application should have an easy user-friendly interface with a main menu that links
to the other screens in the application. This includes:
o a gym membership form where users can add new members
o a screen to search for members
o a screen to book members into a fitness class of their choice.
• There should be an ability to search for members by entering their name and/or
membership type. This would then filter the data (for example, find all members who
have a premium membership).
• After searching for members, there should be an option to clear the search results so
that all members appear again.
• The fitness class booking screen should provide members the option to sign up for a
fitness class. This would allow users to select a week, then select a session on any given
week. Members are able to attend fitness sessions at any one of the following timeslots:
o Spin class: Wednesday 7 am or Friday 6 pm.
o Cardio: Thursday 6 pm or Friday 7 am.
o Pilates: Wednesday 6 pm.
• City Gym have provided you with the details of five members so these can be entered
into the database (see Appendix B for this assignment) and the following information
they would like stored in the database:

o For each Member, keep track of:
- unique member ID
- first name
- last name
- address
- mobile
- payment frequency
- membership expiry date
- any extras the client has signed up for.

o For each Membership, keep track of:
o unique membership ID
o description (for example, Basic)
o cost.
o For each Fitness Class, keep track of:
o unique class ID
o description (for example, Cardio)
o instructor name.
o For each Class Booking, keep track of:
o unique Booking ID
o week start date
o day
o time.
o Each member has one membership, each membership can have many members.
o Each member can make many class bookings, each class booking will be for one
member.

o Each fitness class will have many class bookings, each class booking will be for one
fitness class.

• You should also populate the Membership table with the Membership Type data that is
shown in the sample data in Appendix B for this assignment (for example, Membership
ID = 1, Description = Basic, Cost = 10).

• Staff members will need to be trained on how to use the application. It has been
suggested that a help function is embedded into the application. This could be a menu
item, and/or an additional UI screen. Help instructions should be clear, making it easy
for any user to understand.
