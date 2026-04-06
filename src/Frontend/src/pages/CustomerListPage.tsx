import { Button, Paper, Stack, styled, Table, TableBody, TableCell, tableCellClasses, TableContainer, TableHead, TablePagination, TableRow, TextField, Typography } from "@mui/material";
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
    const [page, setPage] = useState(0);
    const [totalCount, setTotalCount] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);

    useEffect(() => {
        const params = new URLSearchParams({
            skip: String(page * rowsPerPage),
            take: String(rowsPerPage),
        });
        if (searchText) params.set("searchText", searchText);

        fetch(`/api/customers/list?${params.toString()}`)
            .then((res) => res.json())
            .then((data) => {
                setList(data.items as CustomerListQuery[]);
                setTotalCount(data.totalCount);
            });
    }, [searchText, page, rowsPerPage]);

    const handleSearch = () => {
        setPage(0);
        setSearchText(searchInput);
    };

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
                Export ALL as XML
            </Button>
        </Stack>
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650, minHeight: 500 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableHeadCell>Id</StyledTableHeadCell>
                        <StyledTableHeadCell>Name</StyledTableHeadCell>
                        <StyledTableHeadCell>Address</StyledTableHeadCell>
                        <StyledTableHeadCell>Email</StyledTableHeadCell>
                        <StyledTableHeadCell>Phone</StyledTableHeadCell>
                        <StyledTableHeadCell>Iban</StyledTableHeadCell>
                        <StyledTableHeadCell>Code</StyledTableHeadCell>
                        <StyledTableHeadCell>Description</StyledTableHeadCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {list.map((row) => (

                        <TableRow
                            key={row.id}
                            sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                        >
                            <TableCell>{row.id}</TableCell>
                            <TableCell>{row.firstName} {row.lastName}</TableCell>
                            <TableCell>{row.address}</TableCell>
                            <TableCell>{row.email}</TableCell>
                            <TableCell>{row.phone}</TableCell>
                            <TableCell>{row.iban}</TableCell>
                            <TableCell>{row.code}</TableCell>
                            <TableCell>{row.description}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <TablePagination
                component="div"
                count={totalCount}
                page={page}
                rowsPerPage={rowsPerPage}
                rowsPerPageOptions={[5, 10, 25, 50]}
                onPageChange={(_, newPage) => setPage(newPage)}
                onRowsPerPageChange={(e) => {
                    setRowsPerPage(parseInt(e.target.value, 10));
                    setPage(0);
                }}
            />
        </TableContainer>
    </>
}

const StyledTableHeadCell = styled(TableCell)(({ theme }) => ({
    [`&.${tableCellClasses.head}`]: {
        backgroundColor: theme.palette.primary.light,
        color: theme.palette.common.white,
    },
}));