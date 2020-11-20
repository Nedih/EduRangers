import React, {useState}  from 'react';
import axios from 'axios';
import './Profile.css';
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";

export default function Profile(props){

    const [avatar, setAvatar] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [name, setName] = useState("");
    const [isprofessor, setIsProfessor] = useState(false);

    function GetUser() {
      axios({method: 'get',
      url: `https://localhost:44327/api/Account/Profile/?email=${props.email}`,
      headers: {'Content-Type': 'application/json'}})
        .then(
          (result) => {
            console.log(result)
            setName(result.data.Name)
            setEmail(result.data.Email)
            setAvatar(result.data.UserAvatar)
          }
        )
    }

    function handleSubmit(event) {
        event.preventDefault();
        const user = {
                UserAvatar: avatar,
                Email: email,
                Password: password,
                Name: name
            };
        JSON.stringify(user);
        axios({
          method: 'put',
          url: "https://localhost:44327/api/Account/",
          data: JSON.stringify(user),
          maxContentLength: Infinity,
          maxBodyLength: Infinity,
          headers: {'Content-Type': 'application/json'}
        })
          .then(res => {
            console.log("RESPONSE ", res);
            console.log(res.data);
            if(res.data.Succedeed){
              alert("You`ve been succesfully logged in");
            }  
            else alert("Wrong email or password");
          }) 
      }

  GetUser();
    return (
    <div className="Profile">
    <Form onSubmit={handleSubmit}>
        <Form.Group size="lg" controlId="avatar">
            <img src={avatar} width="128px" height="128px"/>
        <Form.Label>Avatar</Form.Label>
        <Form.Control
            type="text"
            value={avatar}
        onChange={(e) => setAvatar(e.target.value)}
        />
        </Form.Group>
        <Form.Group size="lg" controlId="name">
        <Form.Label>Name</Form.Label>
        <Form.Control
            type="text"
            value={name}
        onChange={(e) => setName(e.target.value)}
        />
        </Form.Group>
        <Form.Group size="lg" controlId="email">
        <Form.Label>Email</Form.Label>
        <Form.Control
            autoFocus
            type="email"
            value={email}
        onChange={(e) => setEmail(e.target.value)}
        />
        </Form.Group>
        <Form.Group size="lg" controlId="password">
        <Form.Label>Password</Form.Label>
        <Form.Control
            type="password"
            value={password}
        onChange={(e) => setPassword(e.target.value)}
        />
            </Form.Group>
        <Button block size="lg" type="submit">
        Save
        </Button>
    </Form>
    </div>
    )
}