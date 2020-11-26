import React, {useState, useEffect}  from 'react';
import axios from 'axios';
import './Courses.css';
import Button from "react-bootstrap/Button";
import history from "../GlobalHistory/GlobalHistory"

export default function AddAnswer(props){
    
    const [name, setName] = useState("");
    const [right, setRight] = useState(false);

    function handleCheck(event){
        event.preventDefault();
        console.log(event.target);
        console.log(event.target.value);
        const target = event.target;
        setRight(target.checked);
    }

    function Add(event){
        event.preventDefault();
        const course = {
            AnswerText: name,
            QuestionId: props.match.params.id,
            IsCorrect: right
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
            Answer Name:</label>
            <input type="text" name="Email" onChange={(e) => setName(e.target.value)} />
            <input type="text" name="IsCorrect" onChange={(e) => handleCheck(e)} />
          
          <Button type="submit">Add</Button>
        </form>
      </div>
        </div>
      )
}