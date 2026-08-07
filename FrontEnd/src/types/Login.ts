export interface RegisterDto
{  
     username:string,
    email: string, 
    password:string,
    confirmPassword:string
}
export interface LoginDto
{
    email:string,
    password:string
}