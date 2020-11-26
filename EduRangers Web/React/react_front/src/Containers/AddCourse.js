import React, {useState, useEffect}  from 'react';
import axios from 'axios';
import './Courses.css';
import Button from "react-bootstrap/Button";
import history from "../GlobalHistory/GlobalHistory"

export default function AddCourse(props){
    
    const [name, setName] = useState("");
    const [desc, setDesc] = useState("");

    function Add(event){
        event.preventDefault();
        const course = {
            CourseName: name,
            CourseDescription: desc,
            AuthorEmail: props.match.params.email
        };
        axios({
        method: 'post',
        url: "https://localhost:44327/api/Course/",
        data: JSON.stringify(course),
        maxContentLength: Infinity,
        maxBodyLength: Infinity,
        headers: {'Content-Type': 'application/json'}
        })
        .then(res => {
            console.log("RESPONSE ", res);
            console.log(res.data);
        })
    }

    return(
        <div className="Course">
          <div>
        <form onSubmit={Add}>
          <label>
            Course Name:</label>
            <input type="text" name="Email" onChange={(e) => setName(e.target.value)} />
            <label>Course Description:</label>
            <input type="text" name="Password" onChange={(e) => seDesc(e.target.value)} />
            
          
          <Button type="submit">Add</Button>
        </form>
      </div>
        </div>
      )
}