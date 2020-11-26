import React, {useState, useEffect}  from 'react';
    import axios from 'axios';
    import './Course.css';
    import Form from "react-bootstrap/Form";
    import Button from "react-bootstrap/Button";
    import history from "../GlobalHistory/GlobalHistory"

export default function Course(props){

        const [course, setCourse] = useState();  
        const [name, setName] = useState(); 
        const [desc, setDesc] = useState(); 
        const [isLoading, setIsLoading] = useState(true);
        const [error, setError] = useState();
        
        function GetCourse() {
            axios({method: 'get',
            url: `https://localhost:44327/api/Course/?id=${props.match.params.id}`,
            headers: {'Content-Type': 'application/json'}})
              .then(
                (result) => {
                  console.log(result.data)
                  setCourse(result.data)
                  setIsLoading(false);
                }
              ).catch(error => {setError(error);
                                setIsLoading(false);}) 
          }

          function handleSubmit(event) {
            event.preventDefault();
            const courseModel = {
                    CourseDescription: name,
                    CourseName: desc
                };
            JSON.stringify(courseModel);
            axios({
              method: 'put',
              url: `https://localhost:44327/api/Course/?id=${props.match.params.id}`,
              data: JSON.stringify(courseModel),
              maxContentLength: Infinity,
              maxBodyLength: Infinity,
              headers: {'Content-Type': 'application/json'}
            })
              .then(res => {
                console.log("RESPONSE ", res);
                console.log(res.data);
                if(res.data.Succedeed){
                  alert("The course was changed succesfully");
                }  
                else alert("The course wasn`t changed");
              }) 
          }


        useEffect(() => {
             GetCourse();
        }, []);

        if(isLoading){
            return(
            <p>Loading...</p>
            )
        }
        else if(error){
            return(<p>Error</p>)
        }
        
        

        const Tests = course.Tests.map((item => <ul key = {item.Id}><li>{item.TestName}<Button onClick={() => history.push(`/test/${item.Id}`)}>Edit</Button><Button onClick={() => {axios.delete(`https://localhost:44327/api/Test/?id=${item.Id}`)
        .then(res => {
          console.log(res);
          console.log(res.data);
        })}}>Delete</Button></li><li>{item.TestDescription}</li></ul>));
        console.log({Tests});

        return(
        <div className="Course">
          <Button onClick={() => history.push(`/addtest/${course.Id}`)}>Add test</Button>
          <Form onSubmit={handleSubmit}>
            <Form.Group size="lg" controlId="name">
        <Form.Label><h3>Course</h3></Form.Label>
        <Form.Control
            type="text"
            defaultValue={course.CourseName}
        onChange={(e) => setName(e.target.value)}
        />
        </Form.Group>
        <Form.Group size="lg" controlId="desc">
        <Form.Label><h6>Description</h6></Form.Label>
        <Form.Control
            type="text"
            defaultValue={course.CourseDescription}
        onChange={(e) => setDesc(e.target.value)}
        />
        </Form.Group>
        <Button block size="lg" type="submit">
        Save
        </Button>
    </Form>
        <br />
        <br />
        <h6>Tests:</h6>
        {Tests}
        </div>
        )
    
}