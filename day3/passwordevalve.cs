using System;
using System.IO;
using System.Linq;
class Password{
    static void Main(){
        Console.WriteLine("enter your password");
        string password=Console.ReadLine();
        if(string.IsNullOrEmpty(password)){
            Console.WriteLine("password cannot be null");
            return;
        }
        bool hasDIgit=password.Any(char.IsDigit);
        bool hasUpper=password.Any(char.IsUpper);
        bool hasLower=password.Any(char.IsLower);
        bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
        int score=0;
        if(hasDIgit){
            score++;
        }
        if(hasUpper){
            score++;
        }
        if(hasLower){
            score++;
        }
        if(hasSpecial){
            score++;
        }
        if(password.Length>=8){
            score++;
        }
        if(score>=4){
            Console.WriteLine("Password is valid");
        }else{
            Console.WriteLine("Password is not valid");
        }
        switch(score){
            case 0:
            Console.WriteLine("weak");
            break;
            case 1:
            Console.WriteLine("Average");
            break;
            case 2:
            Console.WriteLine("above average");
            break;
            case 3:
            Console.WriteLine("strong");
            break;
            case 4:
            Console.WriteLine("very strong");
            break;
            case 5:
            Console.WriteLine("perfect");
            break;
            default:
            Console.WriteLine("invalid");
            break;  
        }
        
    }
}