import React, {useState}  from 'react';
import axios from 'axios';
import './Courses.css';
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";

export default function Courses(props){
    const [id, setId] = useState("");
    const [name, setName] = useState("");
    const [desc, setDesc] = useState("");
    const [author, setAuthor] = useState("");
    const [courses, setCourses] = useState([]);

    function GetCourses() {
        axios({method: 'get',
        url: `https://localhost:44327/api/Course/ProfCourses/?email=${props.email}`,
        headers: {'Content-Type': 'application/json'}})
          .then(
            (result) => {
              console.log(result.data)
              setCourses(result.data)
            }
          )
      }
       
      GetCourses();

      const Acha = courses.map((item => <ul key = {item.Id}><li>{item.CourseName}</li><li>{item.CourseDescription}</li></ul>));
      console.log({Acha});

      return(
        <div>
        {Acha}
        </div>
      )

}