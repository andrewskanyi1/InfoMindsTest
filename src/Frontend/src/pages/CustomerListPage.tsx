import { Button, Paper, Stack, styled, Table, TableBody, TableCell, tableCellClasses, TableContainer, TableHead, TableRow, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
interface CustomerListQuery {
    id: number;
    firstName: string | null;
    lastName: string | null;
    address: string | null;
    email: string | null;
    phone: string | null;
    iban: string | null;
    code: string | null;
    description: string | null;
}

export default function CustomerListPage() {
    const [list, setList] = useState<CustomerListQuery[]>([]);
    const [searchInput, setSearchInput] = useState("");
    const [searchText, setSearchText] = useState("");

    useEffect(() => {
        const params = new URLSearchParams({ take: "10" });
        if (searchText) params.set("searchText", searchText);

        fetch(`/api/customers/list?${params.toString()}`)
            .then((res) => res.json())
            .then((data) => setList(data as CustomerListQuery[]));
    }, [searchText]);

    const handleSearch = () => setSearchText(searchInput);

    const handleKeyDown = (e: React.KeyboardEvent) => {
        if (e.key === "Enter") handleSearch();
    };

    const handleExport = () => {
        const params = new URLSearchParams();
        if (searchText) params.set("searchText", searchText);

        fetch(`/api/customers/export?${params.toString()}`)
            .then((res) => res.blob())
            .then((blob) => {
                const url = window.URL.createObjectURL(blob);
                const a = document.createElement("a");
                a.href = url;
                a.download = "customers.xml";
                a.click();
                window.URL.revokeObjectURL(url);
            });
    };

    return <>
        <Typography variant="h4" sx={{ textAlign: "center", mt: 4, mb: 4 }}>
            Customers
        </Typography>

        <Stack direction="row" spacing={1} sx={{ mb: 2 }}>
            <TextField
                size="small"
                label="Search"
                value={searchInput}
                onChange={(e) => setSearchInput(e.target.value)}
                onKeyDown={handleKeyDown}
                placeholder="Name, email..."
                autoComplete="off"
            />
            <Button variant="contained" onClick={handleSearch}>
                Search
            </Button>
            <Button variant="outlined" onClick={handleExport}>
                Export XML
            </Button>
        </Stack>
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableHeadCell>Name</StyledTableHeadCell>
                        <StyledTableHeadCell>Address</StyledTableHeadCell>
                        <StyledTableHeadCell>Email</StyledTableHeadCell>
                        <StyledTableHeadCell>Phone</StyledTableHeadCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {list.map((row) => (
                        <TableRow
                            key={row.id}
                            sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                        >
                            <TableCell>{row.firstName} {row.lastName}</TableCell>
                            <TableCell>{row.address}</TableCell>
                            <TableCell>{row.email}</TableCell>
                            <TableCell>{row.phone}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    </>
}

const StyledTableHeadCell = styled(TableCell)(({ theme }) => ({
    [`&.${tableCellClasses.head}`]: {
        backgroundColor: theme.palette.primary.light,
        color: theme.palette.common.white,
    },
}));