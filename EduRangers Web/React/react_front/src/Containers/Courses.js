import React, {useState, useEffect}  from 'react';
import axios from 'axios';
import './Courses.css';
import Button from "react-bootstrap/Button";
import history from "../GlobalHistory/GlobalHistory"

export default function Courses(props){
    const [courses, setCourses] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
        const [error, setError] = useState();

    function GetCourses() {
        axios({method: 'get',
        url: `https://localhost:44327/api/Course/ProfCourses/?email=${props.match.params.email}`,
        headers: {'Content-Type': 'application/json'}})
          .then(
            (result) => {
              console.log(result.data)
              setCourses(result.data)
              setIsLoading(false);
            }
          ).catch(error => {setError(error);
                            setIsLoading(false);}) 
      }
       
      useEffect(() => {
        GetCourses();
   }, []);

  
   
   if(isLoading){
       return(
       <p>Loading...</p>
       )
   }
   else if(error){
       return(<p>Error</p>)
   }

      //const Acha = courses.map((item => <ul key = {item.Id}><li>{item.CourseName}<LinkContainer to="/course">Edit</LinkContainer></li><li>{item.CourseDescription}</li></ul>));

      return(
        <div className="Course">
          <Button onClick={() => history.push(`/addcourse/${props.match.params.email}`)}>Add course</Button>
          {courses.map((item => <ul key = {item.Id}><li>{item.CourseName}<Button onClick={() => history.push(`/course/${item.Id}`)}>Edit</Button><Button onClick={() => {axios.delete(`https://localhost:44327/api/Course/?id=${item.Id}`)
      .then(res => {
        console.log(res);
        console.log(res.data);
      })}}>Delete</Button></li><li>{item.CourseDescription}</li></ul>))}
        </div>
      )

}