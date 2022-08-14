import React, {SyntheticEvent, useEffect, useState} from "react";
import Layout from "../../components/Layout";
import {Button, Stack, TextField} from "@mui/material";
import axios from "axios";
import {Navigate, useParams} from "react-router-dom";
import {
    LocalizationProvider,
    TimePicker
} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";

const BranchForm = () => {
    const [title, setTitle] = useState("");
    const [managerName, setManagerName] = useState("");
    const [openValue, setOpeningTime] = React.useState<Date | null>(
        new Date('2022-08-13T08:00:00'),
    );
    const [closeValue, setClosingTime] = React.useState<Date | null>(
        new Date('2022-08-13T22:00:00'),
    );

    const [redirect, setRedirect] = useState(false);
    const params = useParams();

    const handleChangeOpen = (newValue: Date | null) => {
        setOpeningTime(newValue);
    };


    const handleChangeClose = (newValue: Date | null) => {
        setClosingTime(newValue);
    };


    const submit = async (e: SyntheticEvent) => {
        e.preventDefault();


        // @ts-ignore
        if (openValue > closeValue)
        {
            alert("invalid time interval");
            return;
        }


        // @ts-ignore
        const openingTime = openValue.toTimeString().split(' ')[0];
        // @ts-ignore
        const closingTime = closeValue.toTimeString().split(' ')[0];


        const data = {
            title,
            managerName,
            openingTime,
            closingTime

        };

        console.log(data)

        await axios.post("branch/addbranch", data);

        setRedirect(true);
    };


    if (redirect) return <Navigate to={"/branch"}/>;

    return (
        <Layout>
            <form onSubmit={submit}>
                <div className="mb-3 mt-3">
                    <TextField
                        value={title}
                        label={"Title"}
                        variant={"standard"}
                        onChange={(e) => {
                            setTitle(e.target.value);
                        }}
                    ></TextField>
                </div>
                <div className="mb-3 mt-3">
                    <TextField
                        value={managerName}
                        onChange={(e) => {
                            setManagerName(e.target.value);
                        }}
                        variant={"standard"}
                        label={"Manager"}
                    ></TextField>


                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                        <Stack className={"mt-5 w-25"} spacing={3}>

                            <TimePicker

                                label="Opening Time"
                                value={openValue}
                                onChange={handleChangeOpen}
                                renderInput={(params) => <TextField {...params} />}
                            />
                            <TimePicker
                                label="Closing Time"
                                value={closeValue}
                                onChange={handleChangeClose}
                                renderInput={(params) => <TextField {...params} />}
                            />
                        </Stack>
                    </LocalizationProvider></div>


                <Button variant={"contained"} color={"primary"} type={"submit"}>
                    Submit
                </Button>
            </form>
        </Layout>
    );
};

export default BranchForm;
