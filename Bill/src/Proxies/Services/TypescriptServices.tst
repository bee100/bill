${
    using Typewriter.Extensions.WebApi;
    Template(Settings settings)
    {
        settings.OutputFilenameFactory = file => 
        {
            var FinalFileName = file.Name.Replace("Controller", "");
            FinalFileName = FinalFileName.Replace(".cs", "");
            return $"{FinalFileName}.service.ts";
        };
    }
    // Change ApiController to Service
    string ServiceName(Class c) => c.Name.Replace("ApiController", "Service");
    // Turn IActionResult into void
    string ReturnType(Method objMethod) 
    {
        if(objMethod.Type.Name == "IActionResult")
        {
                return "void";
        } 
        else
        {
                return objMethod.Type.Name;
        }
    }
    // Get the non primitive paramaters so we can create the Imports at the
    // top of the service
    string ImportsList(Class objClass)
    {
        List<Type> types = new List<Type>();

        var ImportsOutput = "";
        // Get the methods in the Class
        var objMethods = objClass.Methods;
        // Loop through the Methdos in the Class
        foreach(Method objMethod in objMethods)
        {
            // Loop through each Parameter in each method
            foreach(Parameter objParameter in objMethod.Parameters)
            {
                // If the Paramater is not prmitive we need to add this to the Imports
                if(!objParameter.Type.IsPrimitive){
                    types.Add(objParameter.Type);
                }
            }
            if(!objMethod.Type.IsPrimitive && !(objMethod.Type.name == "void" || objMethod.Type.Name == "IActionResult"))
            {
              types.Add(objMethod.Type);
            }
        }

        foreach(var type in types)
        {
          ImportsOutput += $"import {{ { type.Name.Split('[')[0]} }} from '../Entities/{type.Name.Split('[')[0]}';";
        }
        // Notice: As of now this will only return one import
        return  ImportsOutput;
    }
    // Format the method based on the return type
    string MethodFormat(Method objMethod)
    {
        if(objMethod.HttpMethod() == "get"){
            return  $"<{objMethod.Type.Name}>(_Url)";
        } 
        
        if(objMethod.HttpMethod() == "post"){
            string parameterName = "test";
            if(objMethod.Parameters.Count() > 0)
            {
              parameterName = objMethod.Parameters[0].name;
            }
            return  $"<{ReturnType(objMethod)}>(_Url," + parameterName + ")";
        }
        if(objMethod.HttpMethod() == "put"){
            return  $"(_Url, {objMethod.Parameters[1].name})";
        }
        if(objMethod.HttpMethod() == "delete"){
            return  $"(_Url)";
        }
        
        return  $"";
    }
}
${
//The do not modify block below is intended for the outputed typescript files... }
//*************************DO NOT MODIFY**************************
//
//THESE FILES ARE AUTOGENERATED WITH TYPEWRITER AND ANY MODIFICATIONS MADE HERE WILL BE LOST
//PLEASE VISIT http://frhagn.github.io/Typewriter/ TO LEARN MORE ABOUT THIS VISUAL STUDIO EXTENSION
//
//*************************DO NOT MODIFY**************************
import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'; 
import { Observable } from 'rxjs';

$Classes(:Controller)[$ImportsList
@Injectable()
export class $ServiceName {
    constructor(private _httpClient: HttpClient) { }        
    $Methods[
    // $HttpMethod: $Url      
    $name($Parameters[$name: $Type][, ]): Observable<$ReturnType> {
        var _Url = `$Url`;
            return this._httpClient.$HttpMethod$MethodFormat;
    }
    ]
}]
