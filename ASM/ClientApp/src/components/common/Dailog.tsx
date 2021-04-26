import React from 'react';
import { createStyles, Theme, withStyles, WithStyles } from '@material-ui/core/styles';
import Dialog from '@material-ui/core/Dialog';
import MuiDialogTitle from '@material-ui/core/DialogTitle';
import MuiDialogContent from '@material-ui/core/DialogContent';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import Typography from '@material-ui/core/Typography';

const styles = (theme: Theme) =>
    createStyles({
        root: {
            margin: 0,
            padding: theme.spacing(2),
            minWidth: 500
        },
        closeButton: {
            position: 'absolute',
            right: theme.spacing(1),
            top: theme.spacing(1),
            color: theme.palette.grey[500],
        },
    });

export interface DialogTitleProps extends WithStyles<typeof styles> {
    id: string;
    children: React.ReactNode;
    onClose: () => void;
}

const DialogTitle = withStyles(styles)((props: DialogTitleProps) => {
    const { children, classes, onClose, ...other } = props;
    return (
        <MuiDialogTitle disableTypography className={classes.root} {...other}>
            <Typography variant="h6">{children}</Typography>
            {onClose ? (
                <IconButton aria-label="close" className={classes.closeButton} onClick={onClose}>
                    <CloseIcon />
                </IconButton>
            ) : null}
        </MuiDialogTitle>
    );
});

const DialogContent = withStyles((theme: Theme) => ({
    root: {
        padding: theme.spacing(2),
    },
}))(MuiDialogContent);


type DailogProps = { assetName: string, metadata: string, isOpen: boolean, handleClose: (isOpen: boolean) => void }

export class CustomizedDialog extends React.Component<DailogProps> {
    constructor(props: DailogProps) {
        super(props);
    }

    handleClose = () => {
        this.props.handleClose(false);
    };

    public render() {
        return (
            <div>
                <Dialog 
                onClose={this.handleClose} 
                aria-labelledby="customized-dialog-title" 
                open={this.props.isOpen}
                
                >
                    <DialogTitle id="customized-dialog-title" onClose={this.handleClose}>
                        {this.props.assetName}
                    </DialogTitle>
                    <DialogContent>
                        <Typography gutterBottom>
                            {this.props.metadata}
                        </Typography>
                    </DialogContent>
                </Dialog>
            </div>
        );
    }
}
