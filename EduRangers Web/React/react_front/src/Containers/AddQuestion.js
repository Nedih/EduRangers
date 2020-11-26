import React, {useState, useEffect}  from 'react';
import axios from 'axios';
import './Courses.css';
import Button from "react-bootstrap/Button";
import history from "../GlobalHistory/GlobalHistory"

export default function AddQuestion(props){
    
    const [name, setName] = useState("");

    function Add(event){
        event.preventDefault();
        const course = {
            QuestionText: name,
            TestId: props.match.params.id
        };
        axios({
        method: 'post',
        url: "https://localhost:44327/api/Question/",
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
            
          
          <Button type="submit">Add</Button>
        </form>
      </div>
        </div>
      )
}