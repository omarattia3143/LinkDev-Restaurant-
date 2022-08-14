import React, {SyntheticEvent, useEffect, useState} from 'react';
import Layout from "../components/Layout";
import Nav from "../components/Nav";
import {ListItem, ListItemText, MenuItem, Select, Stack, TextField} from "@mui/material";
import axios from "axios";
import {Branch} from "../models/Branch";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DesktopDatePicker, LocalizationProvider} from "@mui/x-date-pickers";
import moment from "moment";

const Booking = () => {

    const [branches, setBranches] = useState<Branch[]>([]);
    const [selectedBranch, setSelectedBranch] = useState("");
    const [pickedDate, setpickedDate] = React.useState<Date | null>(new Date());
    const [timeslots, setTimeslots] = useState<Date[]>([]);
    const [timeslotsFrom, setTimeslotsFrom] = useState("");
    const [timeslotsTo, setTimeslotsTo] = useState("");
    const [name, setName] = useState("");
    const [phone, setPhone] = useState("");
    const [numberOfChairs, setNumberOfChairs] = useState(1);


    useEffect(() => {
        (async () => {
            const {data, headers} = await axios.get("branch/GetAllBranches");
            setBranches(data)

        })();
    }, []);

    // const HandelChange = async () => {
    //
    //     const {data, headers} = await axios.get("branch/GetAllBranches");
    //
    // };


    const handleDateChange = (newValue: Date | null) => {
        setpickedDate(newValue);
    };

    const handleSelection = (e: any) => {
        e.preventDefault();

        setSelectedBranch(e.target.value)


        // setSelectedBranch(e.target.value)

    };

    const handleFromSelection = (e: any) => {
        e.preventDefault();

        setTimeslotsFrom(e.target.value)


    };

    const handleToSelection = (e: any) => {
        e.preventDefault();

        setTimeslotsTo(e.target.value)


    };


    const handleAvailableSlotsSubmit = async (e: any) => {
        e.preventDefault();
        if (pickedDate == null || selectedBranch == "") {
            alert("please fill all the required input")
            return;
        }


        const params = {
            branchId: selectedBranch,
            day: pickedDate.toJSON()
        };

        console.log(params);

        const {data} = await axios.post("booking/AvailableTimeslotsFromBranches", params);

        setTimeslots(data)
        console.log(data);


    };

    const handleBooking = async () => {

            if (timeslotsFrom == "" || timeslotsTo == "" || numberOfChairs == 0 || name == "" || phone == "") {
                alert("please fill all the required input")
                return;
            }

            const params = {
                branchId: selectedBranch,
                username: name,
                phone,
                numberOfChairs,
                pickedDate,
                timeslotsFrom,
                timeslotsTo
            };

            console.log(params);

            const {data} = await axios.post("booking/addbooking", params);

            console.log(data)

        }
    ;

    return (
        <Layout>

            <form onSubmit={handleAvailableSlotsSubmit}>

                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <Stack className={"w-25"}
                           spacing={3}>

                        <label htmlFor="branchSelect">Select a branch</label>
                        <Select labelId={"select"} id={"branchSelect"} value={selectedBranch} fullWidth
                                onChange={(e) => handleSelection(e)}>
                            {branches.map(branch => {
                                return (<MenuItem key={branch.id} value={branch.id}>{branch.title}</MenuItem>)
                            })}

                        </Select>


                        <DesktopDatePicker
                            label="Date desktop"
                            inputFormat="MM/dd/yyyy"
                            value={pickedDate}
                            onChange={handleDateChange}
                            renderInput={(params) => <TextField {...params} />}
                        />

                        <button className="w-100 btn btn-lg btn-primary mt-3" type="submit">
                            Available Slots
                        </button>


                    </Stack>
                </LocalizationProvider>

            </form>

            <form onSubmit={handleBooking}>
                <Stack className={"w-25"}>

                    <div className="mb-3 mt-3">
                        <div className="mb-3 mt-3">
                            <TextField
                                value={name}
                                label={"Name"}
                                variant={"standard"}
                                onChange={(e) => {
                                    setName(e.target.value);
                                }}
                            ></TextField>
                        </div>
                        <TextField
                            value={phone}
                            label={"phone"}
                            variant={"standard"}
                            onChange={(e) => {
                                setPhone(e.target.value);
                            }}
                        ></TextField>
                    </div>

                    <div className="mb-3 mt-3">
                        <TextField
                            value={numberOfChairs}
                            label={"Number of chairs"}
                            variant={"standard"}
                            onChange={(e) => {
                                setNumberOfChairs(parseInt(e.target.value));
                            }}
                        ></TextField>
                    </div>

                    <label htmlFor="branchSelect"> Select a *from* timeslot
                    </label>
                    <Select defaultValue="" labelId={"select"} id={"from"} fullWidth
                            onChange={(e) => handleFromSelection(e)}>
                        {timeslots.map((timeSlot, index) => {


                            return (<MenuItem key={index}
                                              value={new Date(timeSlot).toLocaleTimeString()}>{new Date(timeSlot).toLocaleTimeString()}</MenuItem>)
                        })}

                    </Select>

                    <label htmlFor="branchSelect">Select a *to* timeslot</label>
                    <Select defaultValue="" labelId={"select"} id={"to"} fullWidth
                            onChange={(e) => handleToSelection(e)}>
                        {timeslots.map((timeSlot: Date, index) => {
                            let ss = new Date(timeSlot);

                            return (<MenuItem key={index}
                                              value={new Date(timeSlot).toLocaleTimeString()}>{new Date(timeSlot).toLocaleTimeString()}</MenuItem>)
                        })}

                    </Select>

                    <button className="w-100 btn btn-lg btn-primary mt-3" type="submit">
                        Book Now
                    </button>
                </Stack>

            </form>

        </Layout>
    )
        ;
};

export default Booking;